import React from "react";

const TextInput = ({ name, id, label, onChange, placeHolder, value, error }) => { // Hook
    let wrapperClass = "form-group";
    if (error && error.length > 0) {
        wrapperClass += "has-error";
    }
    return (
        <div className={wrapperClass}>
            <label htmlFor={name} className='label'>{label}</label>
            <div className="field">
                <input
                    name={name}
                    id={id}
                    onChange={onChange}
                    value={value}
                    placeholder={placeHolder}
                    type="text" className="input input-container" />
                {error && <div className="alert alert-danger">{error}</div>}
            </div>
        </div>
    )
};

export default TextInput;