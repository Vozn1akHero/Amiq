import React from 'react';
import {ItemsFrameL} from "../../../../common/components/ItemsFrameL/ItemsFrameL";
import {IIdBasedPersistentData} from "../../../../store/redux/base/id-based-persistent-data";
import {IPaginatedStoreData} from "../../../../store/redux/base/paginated-store-data";
import {IGroupEvent} from "../../models/group-event";

type Props = {
    groupId: number;
    groupEvents: IIdBasedPersistentData<IPaginatedStoreData<IGroupEvent>>;
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
                                    return <div className="group-events-in-frame__item" key={key}>
                                        {value.name}
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