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
                <div className='button-container'>
                    <button type="submit" className='button' >Login</button>
                </div>
            </form>
        </div>
    )
}
