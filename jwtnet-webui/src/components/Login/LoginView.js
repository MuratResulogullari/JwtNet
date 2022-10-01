import React from 'react';
import TextInput from '../toolbox/TextInput';
import './Login.css';

export default function LoginView({ user, onLogin, onChange, errors }) {
    return (
        <div className="login-wrapper app">
            <h2 className='title'>Please Log In</h2>
            <form onSubmit={onLogin} className='login-form' >
                <p className='error'>{errors.loginError}</p>
                <TextInput id="username" name="username" label="Email" value={user.username} onChange={onChange} error={errors.username} />
                <TextInput id="password" name="password" label="password" value={user.password} onChange={onChange} error={errors.password} />

            </form>
            <span>Forgot Password</span>
            <div className='button-container'>
                <button type="submit" className='button' >Login</button>
            </div>
            <p>Or SinUp Using</p>
            <div className='icons'>
                <a href='#'><i className='fab fa-facebook-f'></i></a>
                <a href='#'><i className='fab fa-twitter'></i></a>
                <a href='#'><i className='fab fa-google'></i></a>
            </div>
        </div>
    )
}
