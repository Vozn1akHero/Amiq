import {Link} from "react-router-dom";
import {INavigationLink} from "./INavigationLink";

type Props = {

};
export const Navigation = (props: Props) => {
    const navigationLinks : Array<INavigationLink> = [
        {
            title: "Profil",
            anchor: "/profile"
        },
        {
            title: "Znajomi",
            anchor: "/friends"
        }
    ];

    return (
        <nav className="uk-navbar-container" uk-navbar>
            <div className="uk-navbar-left">
                <ul className="uk-navbar-nav">
                    {/*<li className="uk-active"><a href=""></a></li>*/}
                    {/*<li className="uk-parent"><a href=""></a></li>*/}
                    {
                        navigationLinks.map((value => <li><Link to={value.anchor}>{value.title}</Link></li>))
                    }
                </ul>
            </div>
        </nav>
    );
};

