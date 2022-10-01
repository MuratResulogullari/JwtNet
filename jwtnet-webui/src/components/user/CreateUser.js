import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import TextInput from '../toolbox/TextInput';
import { useParams } from 'react-router-dom';
import { createUser, getUserById, getUsers, updateUser } from '../../redux/actions/userActions';
import { getRoles } from '../../redux/actions/roleActions';
import SelectInput from '../toolbox/SelectInput';

function CreateUser({
    roles,
    getRoles,
    getUserById,
    users,
    getUsers,
    createUser,
    updateUser,
    history,
    ...props
}) {


    const [user, setUser] = useState({ ...props.user });
    const [errors, setErrors] = useState({});
    useEffect(() => {
        if (roles.length === 0) {
            getRoles();
        }
        if (users.length === 0) {
            getUsers();
        }
        setUser({ ...props.user })
    }, [props.user]);

    function handleChange(event) {
        /** create object set target.name and target.value */
        const { name, value } = event.target;
        /** previousProduct = this const [product, setProduct] */
        setUser(previousUser => (
            {
                ...previousUser,
                [name]: value
            }));
        console.log(user);
        validation(name, value);
    }
    function validation(name, value) {
        if (name === "name" && value === "") {
            setErrors(previousErrors => ({ ...previousErrors, name: "User name required." }));
        }
        else {
            setErrors(previousErrors => ({ ...previousErrors, name: "" }));

        }
        if (name === "surname" && value === "") {
            setErrors(previousErrors => ({ ...previousErrors, name: "surname required." }));
        }
        else {
            setErrors(previousErrors => ({ ...previousErrors, name: "" }));

        }
        if (name === "email" && value === "") {
            setErrors(previousErrors => ({ ...previousErrors, email: "email  required." }));
        }
        else {
            setErrors(previousErrors => ({ ...previousErrors, email: "" }));

        }
        if (name === "password" && value === "") {
            setErrors(previousErrors => ({ ...previousErrors, password: "password  required." }));
        }
        else {
            setErrors(previousErrors => ({ ...previousErrors, password: "" }));

        }
    }
    const handleSubmit = async e => {
        e.preventDefault();
        if (user.id > 0) {
            updateUser(user);
        } else {
            createUser(user);
        }
    }
    getUserById(1);
    return (
        <div className="login-wrapper app">
            <h2 className='title'>{user.id ? "Update" : "Create"}</h2>
            <form onSubmit={handleSubmit} className='login-form' >
                <input type="hidden" id="id" name="id" value={user.id} />
                <TextInput id="name" name="name" label="Name" value={user.name} onChange={handleChange} error={errors.name} />
                <TextInput id="surname" name="surname" label="Surname" value={user.surname} onChange={handleChange} error={errors.surname} />
                <TextInput id="email" name="email" label="Email" type='email' value={user.userName} onChange={handleChange} error={errors.email} />
                <TextInput id="password" name="password" label="Password" type='password' value={user.password} onChange={handleChange} error={errors.password} />
                <SelectInput id="roleId" name="roleId" label="Role"
                    value={user.roleName || ""}
                    defaultOption="Choice"
                    options={roles.map(role => ({
                        value: role.id,
                        text: role.roleName

                    }))}
                    onChange={handleChange} error={errors.roleId} />
                <div className='button-container'>
                    <button type="submit" className='button' >Save</button>
                </div>
            </form>
        </div>
    )

}
export function getUser(users, userId) {
    return users.find(x => x.id == userId) || null;
}

function mapStateToProps(state) {
    return {
        user: state.cudRoleReducer,
        users: state.usersReducer,
        roles: state.rolesReducer,

    }

}

const mapDispatchToProps = {
    getRoles,
    getUsers,
    createUser,
    updateUser,
    getUserById,
};

export default connect(mapStateToProps, mapDispatchToProps)(CreateUser);
