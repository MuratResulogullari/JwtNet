import React from 'react';
import TextInput from '../toolbox/TextInput';
import './Login.css';

export default function LoginView({ user, onLogin, onChange, errors }) {
    return (
        <div className="login-wrapper app">
            <h2 className='title'>Please Log In</h2>
            <form onSubmit={onLogin} className='login-form' >
                {/* {this.errors.errorLogin != null ? <p className='error'>this.errors.errorLogin</p> : ""} */}
                <TextInput id="username" name="username" label="Email" value={user.username} onChange={onChange} error={errors.username} className='input-container' />
                <TextInput id="password" name="password" label="password" value={user.password} onChange={onChange} error={errors.password} className='input-container' />
                <div className='button-container'>
                    <button type="submit" >Login</button>
                </div>
            </form>
        </div>
    )
}
