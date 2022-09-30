export default function apiFetch(url, method, body, ...customConfig) {
    // const xtoken = window.sessionStorage.getItem('token');
    //const xtoken = window.localStorage.getItem("xsrfKey")
    const headers = {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Access-Control-Allow-Origin': '*',
    }
    customConfig.credentials = "same-origin";

    let config = {
        method: method ? method : 'GET',
        ...customConfig,
        headers: {
            ...headers,
            ...customConfig.headers,
        },
    }
    if (body) {
        config.body = JSON.stringify(body);
    }
    return fetch(url, config)
        .then(async response => {
            if (response.ok) {
                return response.json();
            } else {
                if (response.status === 401) {
                    //logout()
                    window.location.href = "/login";
                    return
                }
                const errorMessage = await response.status + ' ' + response.statusText + ' ' + response.text;
                throw new Error(errorMessage);
                //return Promise.reject(new Error(errorMessage))
            }
        })
        .catch((error) => {
            console.error("Resulted error : " + error);
            throw error;
        });
}
