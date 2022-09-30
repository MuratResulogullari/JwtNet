import React, { useEffect, useState } from 'react';
import { connect, useSelector } from 'react-redux';
import { createRole, getRoles, getRoleById, updateRole } from '../../redux/actions/roleActions';
import TextInput from '../toolbox/TextInput';
import { useParams } from 'react-router-dom';

function CreateRole({
    roles,
    getRoles,
    createRole,
    updateRole,
    history,
    ...props
}) {


    const [role, setRole] = useState({ ...props.role });
    const [errors, setErrors] = useState({});
    useEffect(() => {
        if (roles.length === 0) {
            getRoles();
        }
        setRole({ ...props.role })
    }, [props.role]);

    function handleChange(event) {
        /** create object set target.name and target.value */
        const { name, value } = event.target;
        /** previousProduct = this const [product, setProduct] */
        setRole(previousRole => (
            {
                ...previousRole,
                [name]: value
            }));
        validation(name, value);
    }
    function validation(name, value) {
        if (name === "roleName" && value === "") {
            setErrors(previousErrors => ({ ...previousErrors, roleName: "Role name required." }));
        }
        else {
            setErrors(previousErrors => ({ ...previousErrors, roleName: "" }));

        }
        if (name === "createdOn" && value === "") {
            setErrors(previousErrors => ({ ...previousErrors, createdOn: "Date  required." }));
        }
        else {
            setErrors(previousErrors => ({ ...previousErrors, createdOn: "" }));

        }
    }
    const handleSubmit = async e => {
        e.preventDefault();
        if (role.id > 0) {
            updateRole(role);

        } else {

            createRole(role);
        }
    }
    return (
        <div className="login-wrapper app">
            <h2 className='title'>{role.id ? "Update" : "Create"}</h2>
            <form onSubmit={handleSubmit} className='login-form' >
                <p className='error'>{errors.loginError}</p>
                <TextInput id="roleName" name="roleName" label="Role Name" value={role.roleName} onChange={handleChange} error={errors.roleName} />
                <TextInput id="createdOn" name="createdOn" label="Date" type='datetime-local' value={role.createdOn} onChange={handleChange} error={errors.createdOn} />
                <TextInput id="isActive" name="isActive" label="Is Active" type='checkbox' value={role.isActive} onChange={handleChange} error={errors.isActive} />
                <div className='button-container'>
                    <button type="submit" className='button' >Save</button>
                </div>
            </form>
        </div>
    )

}
export function getRole(roles, roleId) {
    console.log(roles);
    return roles.find(x => x.id == roleId) || null;
}

function mapStateToProps(state) {
    const { roleId } = useParams();
    return {
        role: roleId && state.rolesReducer.length > 0
            ? getRole(state.rolesReducer, roleId) : {},
        roles: state.rolesReducer,
    }

}

const mapDispatchToProps = {
    getRoles,
    createRole,
    updateRole
};

export default connect(mapStateToProps, mapDispatchToProps)(CreateRole);
