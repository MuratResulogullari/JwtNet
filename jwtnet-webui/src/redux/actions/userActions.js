import * as actionTypes from "./actionTypes";
import apiFetch from "./apiFetch";

/** CRUDs  */

export function createUserSuccess(user) {
    return { type: actionTypes.CREATE_USER_SUCCESS, payload: user }
}
export function updateUserSuccess(user) {
    return { type: actionTypes.UPDATE_USER_SUCCESS, payload: user }
}
export function deleteUserSuccess(user) {
    return { type: actionTypes.DELETE_USER_SUCCESS, payload: user }
}
export const getUsersSuccess = (users) => {
    return { type: actionTypes.GET_USERS_SUCCESS, payload: users };
}
export function getUserByIdSuccess(user) {
    return { type: actionTypes.GET_USER_BY_ID_SUCCESS, payload: user }
}
export const getUsers = () => {
    return async (dispatch) => {
        var url = 'https://localhost:7088/api/User/GetUsers';
        await apiFetch(url, 'GET').then(data => {
            if (data.isSuccess) {
                return dispatch(getUsersSuccess(data.result))
            }
            else {
                console.log("getUsers", error);
            }
        }).catch(handleError)
    }
}
export const getUserById = (userId) => {
    return async function (dispatch) {
        var url = 'https://localhost:7088/api/User/getUserById/' + userId;
        apiFetch(url).then(data => {
            if (data.isSuccess) {
                return dispatch(getUserByIdSuccess(data.result))
            }
            else {
                var error = data.message;
                console.log("getRoleById ", error);
            }
        }).catch(handleError);
    }
}
export const createUser = (user) => {
    return async (dispatch) => {
        var url = 'https://localhost:7088/api/User/Register';
        await apiFetch(url, 'POST', user)
            .then(data => {
                if (data.isSuccess) {
                    return dispatch(createUserSuccess(data.result))

                }
                else {
                    var error = data.message;
                    console.log("createRole ", error);
                }
            }).catch(handleError);
    }
}
export const updateUser = (user) => {
    return async (dispatch) => {
        var url = 'https://localhost:7088/api/User/UpdateUser';
        await apiFetch(url, 'PUT', user)
            .then(data => {
                if (data.isSuccess) {
                    sessionStorage.setItem('token', JSON.stringify(data.result));
                    return dispatch(updateUserSuccess(data.result));
                }
                else {
                    var error = data.message;
                    console.log("updateUser", error);
                }
            }).then(handleError);
    }
}
export const deleteUser = (user) => {
    return async (dispatch) => {
        var url = 'https://localhost:7088/api/User/DeleteUser';
        await apiFetch(url, 'DELETE', user)
            .then(data => {
                if (data.isSuccess) {
                    return dispatch(deleteUserSuccess(data.result))
                }
                else {
                    var error = data.message;
                    console.log("deleteUser ", error);
                }
            }).catch(handleError);
    }
}
/** Errors  */
export async function handleResponse(response) {
    if (response.ok) {
        return response.json();
    }
    else {
        const error = await response.text()
        throw new Error(error);
    }
}
export function handleError(error) {
    console.error("Resulted error : " + error);
    throw error;
}