import React from "react";

const SelectInput = ({ id, name, label, onChange, defaultOption, value, error, options }) => {
    return (
        <div className="form-group">
            <label htmlFor={name}>{label}</label>
            <select id={id} name={name} onChange={onChange} className="form-control">
                <option value="" disabled>{defaultOption}</option>
                {options.map(option => (
                    <option selected={value == option.value ? true : false} key={option.value} value={option.value}>
                        {option.text}
                    </option>
                ))}
            </select>
            {error && <div className="alert alert-danger">{error}</div>}
        </div>
    )
}
export default SelectInput;