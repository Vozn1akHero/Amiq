import React, {useEffect, useState} from 'react';
import UserService from "../../features/user/user-service";
import {AuthStore} from "../../store/custom/auth/auth-store";
import {AxiosResponse} from "axios";
import {useHistory, useLocation} from "react-router-dom";
import BasicUserSettingsSubpage from "./BasicUserSettingsSubpage";
import UserSecuritySettingsSubpage from "./UserSecuritySettingsSubpage";

const UserSettingsPage = () => {
    const location = useLocation();
    const history = useHistory();
    const [initDataLoaded, setInitDataLoaded] = useState(false);
    const [chosenSectionId, setChosenSectionId] = useState(1);
    const userService: UserService = new UserService();

    useEffect(() => {
        rerenderSubpageAfterHashChange();
        getInitialState();
    }, [])

    useEffect(() => {
        rerenderSubpageAfterHashChange();
    }, [location.hash])

    const rerenderSubpageAfterHashChange = () => {
        setChosenSectionId(getTabIndex);
    }

    const getTabIndex = () => {
        let index: number;
        switch (location.hash) {
            case "#basic":
                index = 1;
                break;
            case "#security":
                index = 2;
                break;
            default:
                index = 1;
                break;
        }
        return index;
    }

    const getInitialState = () => {
        userService.getById(AuthStore.identity.userId).then((res: AxiosResponse) => {
            console.log(res)
            setInitDataLoaded(true);
        })
    }

    const navigate = (e, link:string) => {
        e.preventDefault();
        history.push(link);
    }

    return (
        <div className="user-settings-page">
            <div className="uk-margin-medium-top">
                <ul className="uk-child-width-expand" uk-tab="true">
                    <li className={chosenSectionId === 1 ? `uk-active` : ""}>
                        <a href="#" onClick={e=>{navigate(e, "#basic")}}>
                            <span className="uk-margin-small-right" uk-icon="icon:nut"></span> Podstawowe
                        </a>
                    </li>
                    <li className={chosenSectionId === 2 ? `uk-active` : ""}>
                        <a href="#" onClick={e=>{navigate(e, "#security")}} >
                            <span className="uk-margin-small-right" uk-icon="icon:users"></span> Bezpieczeństwo
                        </a>
                    </li>
                </ul>
            </div>

            {
                initDataLoaded && <>
                    {chosenSectionId === 1 && <BasicUserSettingsSubpage />}
                    {chosenSectionId === 2 && <UserSecuritySettingsSubpage email={""} />}
                </>
            }
        </div>
    );
};

export default UserSettingsPage;
