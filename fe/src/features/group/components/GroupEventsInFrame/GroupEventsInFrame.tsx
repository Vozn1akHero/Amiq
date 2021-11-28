import React from 'react';
import {ItemsFrameL} from "../../../../common/components/ItemsFrameL/ItemsFrameL";
import {IIdBasedPersistentData} from "../../../../store/redux/base/id-based-persistent-data";
import {IPaginatedStoreData} from "../../../../store/redux/base/paginated-store-data";
import {IGroupEvent} from "../../models/group-event";
import {Utils} from "../../../../core/utils";
import "./group-events-in-frame.scss"
import {Link} from 'react-router-dom';

type Props = {
    groupId: number;
    groupEvents: IIdBasedPersistentData<IPaginatedStoreData<IGroupEvent>>;
    onGroupEventClick(groupEventId: string): void;
}

const GroupEventsInFrame = (props: Props) => {

    return (
        <ItemsFrameL title="Wydarzenia"
                     displayHeaderAsLink={true}
                     link={"/group/1/events"}
                     icon="calendar">
            <>
                {
                    props.groupEvents.entries.find(e => e.id === props.groupId)?.data.loaded
                    && <div className="group-events-in-frame">
                        {
                            props.groupEvents.entries.find(e => e.id === props.groupId).data.entities.length > 0
                                ? props.groupEvents.entries.find(e => e.id === props.groupId).data.entities.map((value, key) => {
                                    return <div className="group-events-in-frame__item uk-margin-small-top" key={key}>
                                        <a onClick={e => {
                                            e.preventDefault();
                                            props.onGroupEventClick(value.groupEventId);
                                        }
                                        }>
                                            <img className="group-events-in-frame__event-avatar border-radius-50"
                                                 src={Utils.getImageSrc(value.avatarSrc)} alt=""/>
                                        </a>
                                        <div className="uk-margin-small-top uk-margin-left">
                                            <a onClick={e => {
                                                e.preventDefault();
                                                props.onGroupEventClick(value.groupEventId);
                                            }}
                                               className="uk-link-muted">
                                                <span>
                                                    {value.name}
                                                </span>
                                            </a>
                                            <div>
                                                <span uk-icon="users" className="uk-icon"></span>
                                                <span>{value.groupEventParticipantsCount} weźmie udział</span>
                                            </div>
                                        </div>
                                    </div>
                                }) : <span>Brak wydarzeń</span>
                        }
                    </div>
                }
            </>
        </ItemsFrameL>
    );
};

export default GroupEventsInFrame;
