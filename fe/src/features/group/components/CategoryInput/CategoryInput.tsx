import React, {Component, useEffect, useState} from 'react';
import {IGroupCategory} from "../../models/group-category-models";
import "./category-input.scss"

type Props = {

}

const CategoryInput = (props: Props) => {
    class Category extends Component<{id: string,
        text: string,
        isViewCategory: boolean,
        isRemoveBtnVisible: boolean,
        onRemove?: (id: string)=>void,
        onClick?: (id: string)=>void}> {
        render() {
            return <div className="category">
                <button className="category__btn uk-button uk-button-default"
                        style={{
                            borderRadius: "1rem",
                            position: "relative"
                        }}
                        onClick={(e) => {
                                e.preventDefault();
                                if(!this.props.isViewCategory)
                                    this.props.onClick(this.props.id);
                            }
                        }>
                    {this.props.text}
                    {
                        this.props.isRemoveBtnVisible &&
                        <button className="border-radius-50 uk-margin-left uk-button-danger"
                                onClick={() => {
                                    if(this.props.onRemove)
                                        this.props.onRemove(this.props.id);
                                }}>
                            X
                        </button>
                    }
                </button>
            </div>;
        }
    }

    const [foundCategories, setFoundCategories] = useState<Array<IGroupCategory>>([]);
    const [selectedCategories, setSelectedCategories] = useState<Array<IGroupCategory>>([]);
    const selectedCategoriesRef = React.createRef<HTMLDivElement>();
    const inputRef = React.createRef<HTMLInputElement>();

    const categories : Array<IGroupCategory> = [
        {groupCategoryId: "1", viewName: "Piłka nożna"},
        {groupCategoryId: "2", viewName: "Piłka ręczna"},
        {groupCategoryId: "3", viewName: "Muzyka"},
        {groupCategoryId: "4", viewName: "Filmy"},
    ];

    useEffect(() => {
        const nextPadding = selectedCategoriesRef.current.clientWidth + 10;
        console.log(nextPadding)
        inputRef.current.style.paddingLeft = nextPadding + "px";
    }, [selectedCategories])

    const onChange = e => {
        const value : string = e.target.value;
        const foundCategories : Array<IGroupCategory> = categories.filter(category => {
            if(!value) return false;
            return category.viewName.toUpperCase().startsWith(value.toUpperCase());
        })
        setFoundCategories(foundCategories);
    }

    const onCategorySelection = (id: string) => {
        const index = categories.findIndex(e=>e.groupCategoryId===id);
        const selectedCategory = categories[index];
        console.log(selectedCategory)
        setSelectedCategories([selectedCategory, ...selectedCategories]);
    }

    const removeCategory = (id: string) => {
        const nextSelectedCategories = selectedCategories.filter(e=>e.groupCategoryId !== id);
        setSelectedCategories(nextSelectedCategories);
    }

    return (
        <div className="category-input">
            <div className="category-input__wrap">
                <div ref={selectedCategoriesRef} className="category-input__selected-categories">
                    {
                        selectedCategories.map((value, index) => {
                            return <Category id={value.groupCategoryId}
                                             key={index}
                                             isRemoveBtnVisible={true}
                                             isViewCategory={true}
                                             onRemove={removeCategory}
                                             text={value.viewName} />
                        })
                    }
                </div>
                <input ref={inputRef} placeholder="Kategorie" className="uk-input" onChange={onChange} />
            </div>
            {
                foundCategories.length > 0 && <div className="uk-flex uk-margin-small-top">
                    {
                        foundCategories.map((value, index) => {
                            return <Category id={value.groupCategoryId}
                                             key={index}
                                             isRemoveBtnVisible={false}
                                             isViewCategory={false}
                                             text={value.viewName}
                                             onClick={onCategorySelection} />
                        })
                    }
                </div>
            }
        </div>
    );
};

export default CategoryInput;
