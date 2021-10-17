import React, {Component, useEffect, useState} from 'react';
import {IGroupData} from "../../models/group-models";
import {GroupService} from "../../services/group-service";
import {StatusCodes} from "http-status-codes";
import {GroupCreationPopupValidationSchema} from "../group-validation-schema";
import {ErrorMessage, Field, Form, Formik} from "formik";
import * as UIkit from "uikit"
import CategoryInput from "../CategoryInput/CategoryInput";
import {IDescriptionBlock} from "common/models/description-block";

type Props = {
    groupData: IGroupData;
}

const GroupBasicSettings = (props:Props) => {
    //const [areControlsAvailable, setAreControlsAvailable] = useState(false)

    const [name, setName] = useState(props.groupData.name);
    const [description, setDescription] = useState(props.groupData.description);
    const [descriptionBlocks, setDescriptionBlocks] = useState<Array<IDescriptionBlock>>(props.groupData.descriptionBlocks);

    const groupService : GroupService = new GroupService();

    /*useEffect(() => {
    }, [name, description])*/

    const onResetClick = () => {
        setName(props.groupData.name);
        setDescription(props.groupData.description);
    }

    const addNewDescriptionBlock = () => {
        let lastDb = descriptionBlocks[descriptionBlocks.length - 1];
        if(lastDb.content.length === 0){
            return;
        }
        const db : IDescriptionBlock = {
            content: "",
            header: "",
            descriptionBlockId: null
        }
        setDescriptionBlocks([
            ...descriptionBlocks,
            db
        ])
    }

    return (
        <div className="group-basic-settings">
            <Formik
                initialValues={{name, description}}
                onSubmit={(values, {setSubmitting}) => {
                    setSubmitting(true);
                    const dto: Partial<IGroupData> = {
                        name, description
                    };
                    groupService.edit(dto).then(res => {
                        if(res.status === StatusCodes.OK){
                            UIkit.notification({message: 'Zapisano', pos: 'bottom-right'})
                        }
                    }).finally(() => {
                        setSubmitting(false);
                    })
                }}
                validationSchema={GroupCreationPopupValidationSchema}
            >
                {({isSubmitting, errors}) => (
                    <Form>
                        <Field name="name" className="uk-input" placeholder="Nazwa"/>
                        <ErrorMessage name="name" component="div"/>
                        <Field as="textarea" name="description" placeholder="Opis"
                               className="uk-textarea uk-margin-small-top" rows={3}/>
                        <ErrorMessage name="description" component="div"/>
                        <div className="uk-margin-small-top">
                            <CategoryInput />
                        </div>
                        <div className="group-basic-settings__db-blocks uk-margin-medium-top">
                            <button className="uk-button uk-border-rounded" onClick={(e) => {
                                e.preventDefault();
                                addNewDescriptionBlock();
                            }}>
                                <span uk-icon="plus" className="uk-icon"></span>
                            </button>
                            <div className="uk-grid uk-child-width-1-3 uk-grid-margin uk-margin-small-top">
                                {
                                    descriptionBlocks.map((value, index) => {
                                        return <div className="uk-card uk-card-default uk-card-body">
                                            <input defaultValue={value.header}
                                                   className="uk-input"
                                                   placeholder="Tytuł"
                                                   onChange={e => {
                                                       let dB : IDescriptionBlock = descriptionBlocks[index];
                                                       dB.header = e.target.value;
                                                   }}
                                                   minLength={1} />
                                            <textarea defaultValue={value.content}
                                                      style={{resize: "none"}}
                                                      rows={3}
                                                      placeholder="Opis"
                                                      className="uk-textarea uk-margin-small-top"
                                                      onChange={e => {
                                                          let dB : IDescriptionBlock = descriptionBlocks[index];
                                                          dB.content = e.target.value;
                                                      }}
                                                      minLength={1} />
                                        </div>
                                    })
                                }
                            </div>
                        </div>
                        <div className="uk-margin-medium-top">
                            <button className="uk-button uk-button-default"
                                    disabled={isSubmitting}
                                    onClick={onResetClick}>
                                Anuluj
                            </button>
                            <button type="submit"
                                    disabled={isSubmitting}
                                    className="uk-button uk-button-primary uk-margin-small-left">
                                Akceptuj
                            </button>
                        </div>
                    </Form>
                )}
            </Formik>
        </div>
    );
};

export default GroupBasicSettings;
