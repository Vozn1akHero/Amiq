import './frame.scss'

type Props = {
    title: string;
    items: {imageSrc: string, title: string}[];
};
export const ItemsFrameL = (props: Props) => {
    return (
        <div className="uk-card uk-card-default uk-card-body">
            <span className="uk-hea">
                {props.title}
            </span>
            {
                props.items.map(value => <div className="frame-item">
                        <img src={value.imageSrc}/>
                        <span>{value.title}</span>
                    </div>)
            }
        </div>
    );
};