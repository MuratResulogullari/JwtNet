import React from 'react';
import TextInput from '../toolbox/TextInput';
import './Login.css';

export default function LoginView({ user, onLogin, onChange, errors }) {
    return (
        <div className="login-wrapper">
            <h1>Please Log In</h1>
            <form onSubmit={onLogin}>
                <TextInput id="username" name="username" label="Email" value={user.username} onChange={onChange} error={errors.username} />
                <TextInput id="password" name="password" label="password" value={user.password} onChange={onChange} error={errors.password} />
                <div>
                    <button type="submit">Submit</button>
                </div>
            </form>
        </div>
    )
}
