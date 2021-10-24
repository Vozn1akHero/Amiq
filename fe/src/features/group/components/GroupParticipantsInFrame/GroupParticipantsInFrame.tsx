import React, {Component} from 'react';
import {IUserInFrame} from "common/components/ItemsFrameL/IUserInFrame";
import {ItemsFrameL} from "common/components/ItemsFrameL/ItemsFrameL";
import { Link } from 'react-router-dom';
import {Utils} from "core/utils";
import "./group-participants-in-frame.scss"

type Props = {
    items: Array<IUserInFrame>;
}

class GroupParticipantsInFrame extends Component<Props> {
    render() {
        return (
            <div className="group-participants-in-frame">
                <ItemsFrameL title="Uczestnicy"
                             displayHeaderAsLink={true}
                             link={"/group/1/participants"}
                             icon="users"
                             callbackText="Brak uczestników" >
                    {
                        this.props.items.length > 0 && <div className="group-participants-in-frame__items">
                            {
                                this.props.items.map((value, index) => <Link key={index} to={value.link}>
                                    <div className="group-participants-in-frame__item">
                                        <img className="group-participants-in-frame__item__avatar border-radius-50"
                                             src={Utils.getImageSrc(value.imagePath)} />
                                        {/*<span>{value.viewName}</span>*/}
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