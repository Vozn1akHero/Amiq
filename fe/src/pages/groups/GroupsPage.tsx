import React, {Component} from 'react';
import MemoizedGroupCard from "features/group/components/GroupCard";
import {IGroupCard} from "../../features/group/group-models";
import {debounceTime, distinctUntilChanged, filter, fromEvent, map, Subscription} from "rxjs";
import DebounceInput from "../../common/components/DebounceInput/DebounceInput";

type Props = {
    groupsLoaded: boolean;
    groupList: Array<IGroupCard>;
    onSearchInputChange(text:string):void;
    leaveGroup(groupId: number):void;
};

type State = {

};

class GroupsPage extends Component<Props, State> {
    //inputRef: React.RefObject<HTMLInputElement>
    //sub: Subscription

    constructor(props) {
        super(props);

        //this.inputRef = React.createRef();
    }

    componentDidMount() {
        //this.onSearch();
    }

    componentWillUnmount() {
        //this.sub.unsubscribe();
    }

    onSearch = () => {
        /*this.sub = fromEvent(this.inputRef.current, 'keyup').pipe(
            map((event: any) => {
                return event.target.value;
            }),
            debounceTime(700),
            distinctUntilChanged()
        ).subscribe((text: string) => {
            this.props.onSearchInputChange(text);
        });*/
    }

    render() {
        return (
            <div className="friend-list-page">
                <legend className="uk-legend uk-margin-medium-top">Moje grupy</legend>
                <div className="input-search">
                    <div className="uk-margin-medium-top uk-margin-medium-bottom">
                        {/*<input className="uk-input" ref={this.inputRef} type="text" placeholder="Szukaj grup"/>*/}
                        <DebounceInput debounceTime={600} onDebounceInputChange={(e) => this.props.onSearchInputChange(e)} />
                    </div>
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
