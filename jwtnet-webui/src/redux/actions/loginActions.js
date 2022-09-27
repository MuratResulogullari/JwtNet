import * as actionTypes from "./actionTypes";

/** login */
export function loginUserSuccess(resultLogin) {

    return { type: actionTypes.LOGIN_USER_SUCCESS, payload: resultLogin };
}


/** API  */

export function loginUserWepApi(user) {
    var requestInit = {
        method: "POST",
        headers: { "content-type": "application/json" },
        body: JSON.stringify(user) // Then request is string
    }
    return fetch('https://localhost:7088/api/User/Login', requestInit)
        .then(handleResponse).catch(handleError);
}

export function loginUser(user) {
    return function (dispatch) { // our action called in here
        return loginUserWepApi(user).then(result => {
            dispatch(loginUserSuccess(result));
        }).catch(error => { throw error });
    }
}

/** Errors  */
export async function handleResponse(response) {
    if (response.ok) {
        return response.json();
    }
    const error = await response.text()
    throw new Error(error);
}
export function handleError(error) {
    console.error("Resulted error : " + error);

    throw error;
}