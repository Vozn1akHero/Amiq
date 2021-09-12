import React, {Component, useEffect, useState} from 'react';
import ProfilePage from "./ProfilePage";
import {IUserPost} from "../../features/post/models/user-post";
import {UserPostService} from "../../features/post/user-post-service";
import {AuthStore} from "../../store/auth/auth-store";
import {useHistory, useParams, withRouter} from "react-router-dom";
import UserService from "features/user/user-service";
import {IUser} from "features/user/models/user";
import {AxiosResponse} from "axios";


const ProfilePageContainer : React.FC = () => {
    const userPostService = new UserPostService();
    const userService = new UserService();

    const [isViewerProfile, setIsViewerProfile] = useState(null);
    const [userPosts, setUserPosts] = useState<Array<IUserPost>>([]);
    const [userData, setUserData] = useState(null);
    const [userDataLoaded, setUserDataLoaded] = useState(false);
    const [postsLoaded, setPostsLoaded] = useState(false);

    //const [actualProfileId, setActualProfileId] = useState()
    let actualProfileId:number;

    const history = useHistory();
    const {profileId} = useParams<any>();

    useEffect(() => {
        initProfileId();
        getUserData();
        getUserPosts();
    }, [])

    const getUserPosts = () => {
        userPostService.getUserPosts(AuthStore.identity.userId, 1).then(value => {
            const posts = value.data as Array<IUserPost>;
            setUserPosts(posts);
            setPostsLoaded(true);
        });
    }

    const initProfileId = () => {
        console.log(AuthStore.identity)
        if(profileId){
            setIsViewerProfile(AuthStore.identity.userId === profileId);
            actualProfileId = profileId;
        } else {
            setIsViewerProfile(true);
            actualProfileId = AuthStore.identity.userId;
        }
    }

    const getUserData = () => {
        userService.getById(actualProfileId.toString()).then(res => {
            const userData = res.data as IUser;
            setUserData(userData);
            setUserDataLoaded(true);
        })
    }

    const createPost = async (text: string) => {
        const post : Partial<IUserPost> = {
            text,
            authorId: AuthStore.identity.userId
        }
        const result : AxiosResponse<IUserPost> = await userPostService.create(post)
        const {data} = result;
        setUserPosts([...userPosts, data]);
    }

    const deletePost = (postId: number) => {
        //userPostService.
        //setUserPosts(userPosts.filter(value => value.postId !== data.postId));

    }

    return (
        <ProfilePage posts={userPosts}
                     postsLoaded={postsLoaded}
                     userData={userData}
                     userDataLoaded={userDataLoaded}
        />
    );

}

export default ProfilePageContainer;