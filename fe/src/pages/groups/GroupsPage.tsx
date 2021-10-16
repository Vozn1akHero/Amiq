import React, {Component} from 'react';
import MemoizedGroupCard from "features/group/components/GroupCard/GroupCard";
import {IGroupCard} from "features/group/models/group-models";
import DebounceInput from "common/components/DebounceInput/DebounceInput";
import SimpleDropdown from "common/components/SimpleDropdown/SimpleDropdown";
import IDropdownOption from "common/components/SimpleDropdown/IDropdownOption";
import GroupCreationPopup from "features/group/components/GroupCreationPopup/GroupCreationPopup";

type Props = {
    groupsLoaded: boolean;
    groupList: Array<IGroupCard>;
    onSearchInputChange(text:string):void;
    leaveGroup(groupId: number):void;
    onSortDropdownOptionSelection(option: IDropdownOption):void;
};

type State = {
};

class GroupsPage extends Component<Props, State> {
    constructor(props) {
        super(props);
    }

    sortDropdownValues : Array<IDropdownOption> = [{
        id: 0,
        text: "Wszystkie"
    },{
        id: 1,
        text: "Administrowane"
    },{
        id: 2,
        text: "Nieadministrowane"
    },{
        id: 3,
        text: "Ukryte"
    }]

    render() {
        return (
            <div className="groups-page">
                <GroupCreationPopup />

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
                    <SimpleDropdown placeholder="Pokaż"
                                    icon="triangle-down"
                                    options={this.sortDropdownValues}
                                    handleOptionClick={this.props.onSortDropdownOptionSelection} />
                </div>
                <div className="uk-grid uk-child-width-1-3@xl uk-child-width-1-2@l uk-child-width-1-2@m">
                    {
                        this.props.groupsLoaded && (this.props.groupList.length > 0 ? this.props.groupList.map((value, i) =>
                            {
                                return <div key={i} className="uk-margin-top">
                                    <MemoizedGroupCard leaveGroup={this.props.leaveGroup}
                                                       groupCard={value} />
                                </div>
                            }) : <span>Nie znaleziono grup</span>)
                    }
                </div>
            </div>
        );
    }
}

export default GroupsPage;
