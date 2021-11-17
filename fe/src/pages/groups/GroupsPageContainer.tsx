import React, {Component} from 'react';
import {GroupService} from "features/group/services/group-service";
import GroupsPage from "./GroupsPage";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {IGroupCard} from "../../features/group/models/group-models";
import {GroupParticipantService} from "../../features/group/services/group-participant-service";
import {StatusCodes} from "http-status-codes";
import IDropdownOption from "../../common/components/SimpleDropdown/IDropdownOption";
import {IPaginatedStoreData} from "../../store/redux/base/paginated-store-data";
import {IResponseListOf} from "../../core/http-client/response-list-of";

type State = {
    selectedGroupFilterId: number;
    cachedGroupsBeforeSearch: Array<IGroupCard>;
    initialSearch: boolean;
    searchInputLoading: boolean;
    groups: IPaginatedStoreData<IGroupCard>;
}

class GroupsPageContainer extends Component<any, State> {
    readonly groupService: GroupService = new GroupService();
    readonly groupParticipant = new GroupParticipantService();

    constructor(props) {
        super(props);
        this.state = {
            selectedGroupFilterId: 0,
            groups: {
                entities: [],
                loaded: false,
                loading: false,
                currentPage: 1,
                length: 0
            },
            cachedGroupsBeforeSearch: [],
            searchInputLoading: false,
            initialSearch: true,
        }
    }

    componentDidMount() {
        this.getGroups(this.state.selectedGroupFilterId);
    }

    getGroups = (filterType: number) => {
        if(this.state.groups.currentPage === 1){
            this.setState({
                groups: {
                    ...this.state.groups,
                    entities: []
                }
            })
        }

        this.groupParticipant.getUserGroups(this.state.groups.currentPage, filterType).then((res) => {
            const groups = res.data as IResponseListOf<IGroupCard>;
            this.setState({
                groups: {
                    entities: [...this.state.groups.entities, ...groups.entities],
                    loading: false,
                    loaded: true,
                    currentPage: this.state.groups.currentPage + 1,
                    length: groups.length
                },
            })
        })
    }

    leaveGroup = (groupId: number) => {
        this.groupParticipant.leaveGroup(AuthStore.identity.userId, groupId).then(res => {
            if (res.status === StatusCodes.OK) {
                this.setState({
                    groups: {
                        ...this.state.groups,
                        entities: this.state.groups.entities.filter(e => e.groupId !== groupId)
                    }
                })
            }
        })
    }

    joinGroup = (groupId: number) => {
        this.groupParticipant.joinGroup(groupId).then(res => {
            if (res.status === StatusCodes.OK) {
                this.setState({
                    groups: {
                        ...this.state.groups,
                        entities: this.state.groups.entities.map(value => {
                            if (value.groupId === groupId) {
                                value.isRequestCreatorParticipant = true;
                            }
                            return value;
                        })
                    }
                })
            }
        })
    }

    onSearchInputChange = (text: string) => {
        if (!text && !this.state.initialSearch) {
            this.setState({
                groups: {
                    ...this.state.groups,
                    entities: this.state.cachedGroupsBeforeSearch
                }
            })
        }
        if (text?.length > 0) {
            this.setState({
                searchInputLoading: true
            })
            this.groupService.getByName(text).then(res => {
                if (this.state.initialSearch) {
                    this.setState({
                        cachedGroupsBeforeSearch: this.state.groups.entities,
                        initialSearch: false
                    }, () => {
                        this.setState({
                            groups: {
                                ...this.state.groups,
                                entities: res.data as Array<IGroupCard>
                            }
                        })
                    })
                } else {
                    this.setState({
                        groups: {
                            ...this.state.groups,
                            entities: res.data as Array<IGroupCard>
                        }
                    })
                }
            }).finally(() => {
                this.setState({
                    searchInputLoading: false
                })
            })
        }
    }

    onSortDropdownOptionSelection = (option: IDropdownOption) => {
        this.setState({
            selectedGroupFilterId: option.id,
            groups:{
                ...this.state.groups,
                currentPage: 1
            }
        }, () =>{
            this.getGroups(option.id);
        })
    }

    createGroup = (groupData: Pick<IGroupCard, 'name' & 'description'>) => {
        this.groupService.create(groupData).then(res => {
            if (res.status === StatusCodes.CREATED) {
                const createdGroup: IGroupCard = res.data;
                this.setState({
                    groups: {
                        ...this.state.groups,
                        entities: [createdGroup, ...this.state.groups.entities],
                        length: this.state.groups.length+1
                    }
                })
            }
        })
    }

    render() {
        return (
                <GroupsPage groups={this.state.groups}
                            getMoreGroups={() => {
                                this.getGroups(this.state.selectedGroupFilterId);
                            }}
                            createGroup={this.createGroup}
                            leaveGroup={this.leaveGroup}
                            joinGroup={this.joinGroup}
                            searchInputLoading={this.state.searchInputLoading}
                            onSortDropdownOptionSelection={this.onSortDropdownOptionSelection}
                            onSearchInputChange={this.onSearchInputChange}/>
        );
    }
}

export default GroupsPageContainer;
