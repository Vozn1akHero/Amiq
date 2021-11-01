import React, {Component} from 'react';
import {IUserInFrame} from "common/components/ItemsFrameL/IUserInFrame";
import {ItemsFrameL} from "common/components/ItemsFrameL/ItemsFrameL";
import {Link} from 'react-router-dom';
import {Utils} from "core/utils";
import "./group-participants-in-frame.scss"
import {IPaginatedStoreData} from "../../../../store/redux/base/paginated-store-data";
import {IGroupParticipant} from "../../models/group-models";

type Props = {
    //items: Array<IUserInFrame>;
    groupParticipants: IPaginatedStoreData<IGroupParticipant>;
}

class GroupParticipantsInFrame extends Component<Props> {
    getConvertedParticipantsToFrameItem = () => {
        if (this.props.groupParticipants.loaded) {
            let arr: Array<IUserInFrame> = [];
            this.props.groupParticipants.entities.slice(0, 6).map(e => {
                arr.push({
                    userId: e.userId,
                    viewName: e.name + " " + e.surname,
                    imagePath: e.avatarPath,
                    link: "/profile/" + e.userId
                })
            })
            return arr;
        }
    }

    render() {
        return (
            <div className="group-participants-in-frame">
                <ItemsFrameL title="Uczestnicy"
                             displayHeaderAsLink={true}
                             link={"/group/1/participants"}
                             icon="users"
                             callbackText="Brak uczestników">
                    {
                        this.props.groupParticipants.loaded && <div className="group-participants-in-frame__items">
                            {
                                this.getConvertedParticipantsToFrameItem().map((value, index) =>
                                    <Link key={index} to={value.link}>
                                        <div className="group-participants-in-frame__item">
                                            <img className="group-participants-in-frame__item__avatar border-radius-50"
                                                 src={Utils.getImageSrc(value.imagePath)}/>
                                        </div>
                                    </Link>)
                            }
                        </div>
                    }
                </ItemsFrameL>
            </div>
        );
    }
}

export default GroupParticipantsInFrame;