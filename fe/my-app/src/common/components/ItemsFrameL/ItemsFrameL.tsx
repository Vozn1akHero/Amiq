import './items-frame-l.scss'

type Props = {
    title: string;
    items: {imageSrc: string, title: string}[];
    callbackText: string;
};
export const ItemsFrameL = (props: Props) => {
    return (
        <div className="uk-card uk-card-default uk-card-body items-frame-l">
            <span className="uk-card-header">
                {props.title}
            </span>
            <div className="items-wrapper uk-margin-small-top">
                {
                    props.items.length > 0 ?
                        props.items.map(value => <div className="frame-item">
                            <img src={value.imageSrc}/>
                            <span>{value.title}</span>
                        </div>) : <span>{props.callbackText}</span>
                }
            </div>
        </div>
    );
};
