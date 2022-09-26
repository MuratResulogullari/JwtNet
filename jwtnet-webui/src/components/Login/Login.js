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
    async function loginUser() {
        var requestInit = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',

            },
            body: JSON.stringify(user) // Then request is string
        }
        fetch('https://localhost:7088/api/User/Login', requestInit)
            .then((response) => response.json())
            .then((data) => {
                if (!data.isSuccess) {
                    console.error('Error:', data.message);
                    setErrors(previousErrors => ({ ...previousErrors, loginError: data.message }));
                    return 0;
                }
                else {
                    setToken(data);
                    return 1;
                }

            })
            .catch((error) => {
                console.error('Error:', error);
            });
    }
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
        const result = await loginUser();
        console.log(result);
        if (result) {
            window.location.href = '/home';
        }


    }
    /** Get view in here */
    return (
        <LoginView user={user} onChange={handleChange} onLogin={handleLogin} errors={errors} />
    )
}
Login.propTypes = {
    setToken: PropTypes.func.isRequired
};
