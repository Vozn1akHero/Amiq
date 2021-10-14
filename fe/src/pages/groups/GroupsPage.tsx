import React, {Component} from 'react';
import MemoizedGroupCard from "features/group/components/GroupCard";
import {IGroupCard} from "../../features/group/group-models";
import DebounceInput from "../../common/components/DebounceInput/DebounceInput";
import SimpleDropdown from "../../common/components/SimpleDropdown/SimpleDropdown";
import CenteredPopup from "../../common/components/CenteredPopup/CenteredPopup";
import IDropdownOption from "../../common/components/SimpleDropdown/IDropdownOption";

type Props = {
    groupsLoaded: boolean;
    groupList: Array<IGroupCard>;
    onSearchInputChange(text:string):void;
    leaveGroup(groupId: number):void;
};

type State = {
};

class GroupsPage extends Component<Props, State> {
    constructor(props) {
        super(props);
    }

    sortDropdownValues : Array<IDropdownOption> = [{
        id: 1,
        text: "Wszystkie"
    },{
        id: 2,
        text: "Administrowane"
    },{
        id: 3,
        text: "Nieadministrowane"
    },{
        id: 4,
        text: "Ukryte"
    }
    ]

    handleSortClick = (option: IDropdownOption) => {

    }

    render() {
        return (
            <div className="groups-page">
                <CenteredPopup id="new-group-popup" title="Utwórz grupę" controlsVisible={true} />

                <legend className="uk-legend uk-margin-medium-top">Moje grupy</legend>
                <div className="input-search">
                    <div className="uk-margin-medium-top uk-margin-medium-bottom">
                        <DebounceInput debounceTime={600}
                                       onDebounceInputChange={(e) => this.props.onSearchInputChange(e)} />
                    </div>
                </div>
                <div className="groups-page__configuration-controls uk-flex">
                    <button className="groups-page__create-btn uk-button uk-button-primary uk-margin-small-right" uk-toggle="target: #new-group-popup">
                        Utwórz grupę
                    </button>
                    <SimpleDropdown options={this.sortDropdownValues}
                                    handleOptionClick={this.handleSortClick} />
                </div>
                <div className="uk-grid uk-child-width-1-3">
                    {
                        this.props.groupsLoaded && this.props.groupList.map((value, i) =>
                            {
                                return <div key={i} className="uk-margin-top">
                                    <MemoizedGroupCard leaveGroup={this.props.leaveGroup}
                                                       groupCard={value} />
                                </div>
                            }
                        )
                    }
                </div>
            </div>
        );
    }
}

export default GroupsPage;
