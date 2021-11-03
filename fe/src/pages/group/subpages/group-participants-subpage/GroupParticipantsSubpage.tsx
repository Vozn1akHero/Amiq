import React, {Dispatch, useEffect, useState} from 'react';
import {useDispatch, useSelector} from "react-redux";
import {getGroupParticipants} from "../../../../store/redux/actions/groupParticipantActions";
import {useParams} from "react-router-dom";
import {IGroupParticipant} from "../../../../features/group/models/group-models";
import {IPaginatedStoreData} from "../../../../store/redux/base/paginated-store-data";
import InfiniteScroll from 'react-infinite-scroll-component';
import UserCard from "../../../../features/user/components/UserCard/UserCard";
import UiKitDefaultSpinner from "../../../../common/components/UIKitDefaultSpinner/UIKitDefaultSpinner";
import SearchInput from "../../../../common/components/SearchInput/SearchInput";
import MainEntitySubpage from "../../../main-entity-subpage";
import {Routes} from "../../../../core/routing";
import "./group-participants-subpage.scss"

const GroupParticipantsSubpage = () => {
    const dispatch: Dispatch<any> = useDispatch();
    const {groupId} = useParams<any>();
    const [showInputSearchSpinner, setShowInputSearchSpinner] = useState(false);

    useEffect(() => {
        dispatch(getGroupParticipants(groupId, groupParticipants.currentPage))
    }, [])

    const groupParticipants: IPaginatedStoreData<IGroupParticipant> = useSelector(
        (state: any) => {
            return state.groupParticipant.groupParticipants
        }
    )

    const search = (text: string) => {

    }

    return (
        <div className="group-participants-subpage">
            <MainEntitySubpage goBackLink={`/${Routes.groupPageRoutes.baseLink}/${groupId}`}>
                <SearchInput onDebounceInputChange={search} showSpinner={showInputSearchSpinner}/>

                {
                    groupParticipants.loaded &&
                    <InfiniteScroll
                        dataLength={groupParticipants.length}
                        next={() => {
                            dispatch(getGroupParticipants(groupId, groupParticipants.currentPage))
                        }}
                        hasMore={groupParticipants.length > groupParticipants.entities.length}
                        loader={<UiKitDefaultSpinner/>}
                    >
                        <div className="group-participants-subpage__list uk-margin-medium-top" >
                            {
                                groupParticipants.entities.map((groupParticipant: IGroupParticipant, index: number) => {
                                    return <UserCard key={index}
                                                     userId={groupParticipant.userId}
                                                     surname={groupParticipant.surname}
                                                     name={groupParticipant.name}
                                                     avatarPath={groupParticipant.avatarPath}
                                                     controls={[]}/>
                                })
                            }
                        </div>
                    </InfiniteScroll>
                }

            </MainEntitySubpage>
        </div>
    );
}

export default GroupParticipantsSubpage;