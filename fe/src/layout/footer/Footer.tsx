import React, {Component, createRef} from 'react';
import "./footer.scss"

type Props = {
    footerHeight: number;
    footerShouldHaveAbsolutePosition: boolean;
    styles?: {[key:string]: string}
}

class Footer extends Component<Props, any> {
    footerRef = createRef<HTMLElement>();

    shouldComponentUpdate(nextProps: Readonly<Props>, nextState: Readonly<any>, nextContext: any): boolean {
        this.setFooterStylesBasedOnProps(nextProps.footerShouldHaveAbsolutePosition);
        return true;
    }

    setFooterStylesBasedOnProps = (footerShouldHaveAbsolutePosition: boolean) => {
        console.log(footerShouldHaveAbsolutePosition)
        if(footerShouldHaveAbsolutePosition){
            console.log(1)
            this.footerRef.current.style.height = this.props.footerHeight + "px";
            this.footerRef.current.style.position = "absolute"
            this.footerRef.current.style.bottom = "0"
        } else {
            this.footerRef.current.style.height = this.props.footerHeight + "px";
            this.footerRef.current.style.position = "relative"
        }
    }

    render() {
        return (
            <footer className="footer uk-text-center" style={this.props.styles} ref={this.footerRef}>
                <span className="main-footer-text">Projekt zrealizowany w ramach pracy magisterskiej</span>
            </footer>
        );
    }
}

export default Footer;