import React, { useState, useEffect } from 'react';
import PropTypes from 'prop-types';
import LoginView from './LoginView';
import './Login.css';

export default function Login({ setToken, loginUser, history, ...props }) {

    //Destructuring assignment
    const [user, setUser] = useState({ ...props.user });
    const [errors, setErrors] = useState({});
    useEffect(() => {
        setUser({ ...props.user })
    }, [props.user]
    );

    /** async function for request wepapi for login */
    async function loginUser(user) {
        var requestInit = {
            method: "POST",
            headers: { "content-type": "application/json" },
            body: JSON.stringify(user) // Then request is string
        }
        return fetch('https://localhost:7088/api/User/Login', requestInit)
            .then(handleResponse).catch(handleError);
    }

    /** Response Error Controller */
    async function handleResponse(response) {
        if (response.ok) {
            return response.json();
        }
        const error = await response.text()
        throw new Error(error);
    }
    function handleError(error) {
        console.error("Resulted error : " + error);
        throw error;
    }
    /**End Errors  */

    /** form elements event for preparete login form  */
    function handleChange(event) {
        const { name, value } = event.target;
        setUser(previousUser => (
            {
                ...previousUser,
                [name]: value
            }));
        validation(name, value);
    }
    /** validation for text dont null */
    function validation(name, value) {
        if (name === "username" && value === "") {
            setErrors(previousErrors => ({ ...previousErrors, username: "Username required." }));
        }
        else {
            setErrors(previousErrors => ({ ...previousErrors, username: "" }));
        }
        if (name === "password" && value === "") {
            setErrors(previousErrors => ({ ...previousErrors, password: "password required." }));
        }
        else {
            setErrors(previousErrors => ({ ...previousErrors, password: "" }));
        }
    }
    const handleLogin = async e => {
        e.preventDefault();
        console.log(user);
        const token = await loginUser({
            "UserName": user.username,
            "Password": user.password
        });
        console.log(token);
        setToken(token);

    }
    /** Get view in here */
    return (
        <LoginView user={user} onChange={handleChange} onLogin={handleLogin} errors={errors} />
    )
}
Login.propTypes = {
    setToken: PropTypes.func.isRequired
};
// function mapStateToProps(state) {

//     return {
//         user: state.user
//     }
// }
// const mapDispatchToProps = {
//     loginUser,
// }
// export default connect(mapStateToProps, mapDispatchToProps)(Login);

// Login.propTypes = {
//     setToken: PropTypes.func.isRequired
// };