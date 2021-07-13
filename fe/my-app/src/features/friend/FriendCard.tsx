type FriendCardProps = {
    avatarSrc: string;
    viewName: string;
}

const FriendCard = (props: FriendCardProps) => {
    const avatarBgStyles : any = {
        backgroundImage: "url(" + props.avatarSrc + ")",
        backgroundPosition: "top center",
        backgroundSize: "cover",
        filter: "blur(15px)",
        opacity: "0.6",
        position: "absolute",
        top: 0,
        left: 0,
        height: "100%",
        width: "100%",
        zIndex: -1,
        transform: "scale(1.2)"
    }

    const nameStyles: any = {
        fontWeight: "bold",
        marginTop: "15px"
    }

    const nameWrapStyles: any = {
        width: "100%",
        height: "24%",
        position: "absolute",
        left: 0,
        bottom: 0,
        paddingLeft: "42px",
        background: "white"
    }

    return (
        <div className="uk-card uk-card-default uk-card-body" style={{zIndex: 1, overflow: "hidden"}}>
            <div className="avatarBg" style={avatarBgStyles}></div>

            <img style={{borderRadius: '50%', border: "3px solid white", marginBottom: "3rem"}}
                 src={props.avatarSrc}
                 sizes="(min-width: 120px) 120px, 100vw" width="120" height="120" alt="" />

            <div className="uk-margin-medium-top name-wrap" style={nameWrapStyles}>
                <h3 className="uk-card-title" style={nameStyles}>{props.viewName}</h3>
            </div>
        </div>
    );
}

export default FriendCard;
