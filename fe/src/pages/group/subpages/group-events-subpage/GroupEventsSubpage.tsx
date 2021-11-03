import React, {Dispatch, useEffect, useState} from 'react';
import {Routes} from "../../../../core/routing";
import SearchInput from "../../../../common/components/SearchInput/SearchInput";
import MainEntitySubpage from "../../../main-entity-subpage";
import {useDispatch, useSelector} from "react-redux";
import {useParams} from "react-router-dom";
import {getGroupEvents} from "../../../../store/redux/actions/groupEventActions";
import {IIdBasedPersistentData} from "../../../../store/redux/base/id-based-persistent-data";
import {IPaginatedStoreData} from "../../../../store/redux/base/paginated-store-data";
import {IGroupEvent} from "../../../../features/group/models/group-event";
import GroupEventCard from "../../../../features/group/components/GroupEventCard/GroupEventCard";
import UiKitDefaultSpinner from "../../../../common/components/UIKitDefaultSpinner/UIKitDefaultSpinner";
import InfiniteScroll from "react-infinite-scroll-component";
import "./group-events-subpage.scss";

const GroupEventsSubpage = () => {
    const dispatch: Dispatch<any> = useDispatch();
    const {groupId} = useParams<any>();
    const [showInputSearchSpinner, setShowInputSearchSpinner] = useState(false)

    useEffect(() => {
        dispatch(getGroupEvents(groupId, 1, 10));
    }, [])

    const allGroupEvents: IIdBasedPersistentData<IPaginatedStoreData<IGroupEvent>> = useSelector(
        (state: any) => {
            return state.groupEvent.groupEvents
        }
    )
    const getCurrentGroupEvents = (): IPaginatedStoreData<IGroupEvent> => {
        return allGroupEvents.entries.find(e => e.id === groupId).data as IPaginatedStoreData<IGroupEvent>
    }

    const search = (text: string) => {

    }

    const onReopenClick = (groupEventId: string) => {

    }

    const onHideClick = (groupEventId: string) => {

    }

    const onMakeVisibleClick = (groupEventId: string) => {

    }

    const onCancelClick = (groupEventId: string) => {

    }

    return (
        <div className="group-events-subpage">
            <MainEntitySubpage goBackLink={`/${Routes.groupPageRoutes.baseLink}/${groupId}`}>
                <SearchInput onDebounceInputChange={search} showSpinner={showInputSearchSpinner}/>
                {
                    allGroupEvents.entries.find(e => e.id === groupId)?.data.loaded &&
                    <InfiniteScroll
                        dataLength={getCurrentGroupEvents().length}
                        next={() => {
                            dispatch(getGroupEvents(groupId, getCurrentGroupEvents().currentPage, 10))
                        }}
                        hasMore={getCurrentGroupEvents().length > getCurrentGroupEvents().entities.length}
                        loader={<UiKitDefaultSpinner/>}
                    >
                        <div className="group-events-subpage__events-list uk-margin-medium-top">
                            {
                                getCurrentGroupEvents().entities.filter(value => {
                                    return !value.isHidden
                                }).map((value, index) => {
                                    return <GroupEventCard key={index}
                                                           eventPageRoute={`/group/${groupId}/event/${value.groupEventId}`}
                                                           groupEventData={value}
                                                           />
                                })
                            }
                        </div>
                    </InfiniteScroll>
                }
            </MainEntitySubpage>
        </div>
    );
};

export default GroupEventsSubpage;
