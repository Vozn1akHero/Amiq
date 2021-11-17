import React, {Component} from 'react';
import MemoizedGroupCard from "features/group/components/GroupCard/GroupCard";
import {IGroupCard} from "features/group/models/group-models";
import SearchInput from "common/components/SearchInput/SearchInput";
import SimpleDropdown from "common/components/SimpleDropdown/SimpleDropdown";
import IDropdownOption from "common/components/SimpleDropdown/IDropdownOption";
import GroupCreationPopup from "features/group/components/GroupCreationPopup/GroupCreationPopup";
import InfiniteScroll from "react-infinite-scroll-component";
import {IPaginatedStoreData} from "../../store/redux/base/paginated-store-data";
import "./groups-page.scss";
import {ModalService} from "../../core/modal-service";

type Props = {
    groups: IPaginatedStoreData<IGroupCard>;
    onSearchInputChange(text: string): void;
    leaveGroup(groupId: number): void;
    joinGroup(groupId: number): void;
    createGroup(groupData: Pick<IGroupCard, 'name' & 'description'>): void;
    onSortDropdownOptionSelection(option: IDropdownOption): void;
    searchInputLoading: boolean;
    getMoreGroups(): void;
};

type State = {};

class GroupsPage extends Component<Props, State> {
    sortDropdownValues: Array<IDropdownOption> = [{
        id: 0,
        text: "Wszystkie"
    }, {
        id: 1,
        text: "Administrowane"
    }, {
        id: 2,
        text: "Nieadministrowane"
    }, {
        id: 3,
        text: "Ukryte"
    }]

    toggleGroupVisibility = (groupId: number, isVisible: boolean) => {

    }

    createGroup = (groupData: Pick<IGroupCard, 'name' & 'description'>) => {
        ModalService.close();
        this.props.createGroup(groupData);
    }

    openCreateGroupModal = () => {
        ModalService.open(<GroupCreationPopup createGroup={this.createGroup}/>);
    }

    render() {
        return (
            <div className="groups-page">
                <legend className="uk-legend uk-margin-medium-top">Moje grupy</legend>
                <div className="input-search">
                    <div className="uk-margin-medium-top uk-margin-medium-bottom">
                        <SearchInput debounceTime={600}
                                     showSpinner={this.props.searchInputLoading}
                                     onDebounceInputChange={(e) => this.props.onSearchInputChange(e)}/>
                    </div>
                </div>
                <div className="groups-page__configuration-controls uk-flex">
                    <button className="groups-page__create-btn uk-button uk-button-primary uk-margin-small-right" onClick={this.openCreateGroupModal}>
                        Utwórz grupę
                    </button>
                    <SimpleDropdown placeholder="Pokaż"
                                    isStatic={true}
                                    icon="triangle-down"
                                    options={this.sortDropdownValues}
                                    handleOptionClick={this.props.onSortDropdownOptionSelection}/>
                </div>
                {
                    this.props.groups.loaded && (this.props.groups.entities.length > 0 ?
                        <InfiniteScroll next={this.props.getMoreGroups}
                                        style={{overflow: 'hidden'}}
                                        hasMore={this.props.groups.entities.length < this.props.groups.length}
                                        loader={<h3>Loading...</h3>}
                                        dataLength={this.props.groups.length}>
                            <div className="groups-page__list">
                                {
                                    this.props.groups.entities.map((value, i) =>
                                    {
                                        return <div key={i} className="uk-margin-top">
                                            <MemoizedGroupCard leaveGroup={this.props.leaveGroup}
                                                               joinGroup={this.props.joinGroup}
                                                               toggleGroupVisibility={this.toggleGroupVisibility}
                                                               groupCard={value} />
                                        </div>
                                    })
                                }
                            </div>
                        </InfiniteScroll> : <span>Nie znaleziono grup</span>)
                }
            </div>
        );
    }


}

export default GroupsPage;
