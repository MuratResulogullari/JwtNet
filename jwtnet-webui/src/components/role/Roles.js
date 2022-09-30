import React, { Component } from 'react'
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as roleActions from '../../redux/actions/roleActions';
import { Link } from 'react-router-dom';
class Roles extends Component {
    componentDidMount() {
        this.props.actions.getRoles();
    }
    updateRole(role) {
        this.props.actions.updateRole(role);
        console.log(this.props.role);

    }
    deleteRole(role) {
        this.props.actions.deleteRole(role);
        console.log(this.props.role);

    }
    getRoleById(role) {
        this.props.actions.getRoleById(role.id);
        console.log(this.props.role);
    }
    renderUpdate() {
        return (
            <div>
                {this.props.roles.length > 0 ? this.renderRoles() : this.renderEmpty()}
            </div>
        )
    }
    renderRoles() {
        return (
            <div className='container'>
                <div className='header d-flex justify-content-between'>
                    <div><h2>Role Managment</h2></div>
                    <div><Link className='btn btn-outline-success' to={"/saverole"}>New Role</Link></div>
                </div>
                <div className='body'>
                    <table className='table'>
                        <thead>
                            <tr>
                                <th>#</th>
                                <th>Role</th>
                                <th>Active</th>
                                <th>CreateOn</th>
                                <th>Process</th>
                            </tr>
                        </thead>
                        <tbody className='table-group-divider'>
                            {this.props.roles.map((role) => (
                                <tr key={role.id}>
                                    <td>{role.id}</td>
                                    <td>{role.roleName}</td>
                                    <td>{role.isActive.toString()}</td>
                                    <td>{role.createdOn}</td>
                                    <td className=' d-flex justify-content-around'>
                                        <button className='btn btn-outline-secondary' onClick={() => this.getRoleById(role)}><i className='fa-thin fa-eye'></i></button>
                                        <Link className='btn btn-outline-warning' to={"/saverole/" + role.id}><i className='fa-thin fa-pen-to-square'></i></Link>
                                        <button className='btn btn-outline-danger' onClick={() => this.deleteRole(role)}><i className='fa-thin fa-xmark'></i></button>

                                    </td>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            </div >
        )
    }
    renderEmpty() {
        return (
            <div className='container'>
                <div class="alert alert-warning" role="alert">
                    Role is not find
                </div>
            </div>
        )
    }
    render() {
        return (
            <div>
                {this.props.roles.length > 0 ? this.renderRoles() : this.renderEmpty()}
            </div>
        )
    }
}

function mapDispatchToProps(dispatch) {
    return {
        actions: {
            getRoles: bindActionCreators(roleActions.getRoles, dispatch),
            getRoleById: bindActionCreators(roleActions.getRoleById, dispatch),
            createRole: bindActionCreators(roleActions.createRole, dispatch),
            updateRole: bindActionCreators(roleActions.updateRole, dispatch),
            deleteRole: bindActionCreators(roleActions.deleteRole, dispatch),

        }
    }
};
function mapStateToProps(state) {

    return {
        roles: state.rolesReducer,
        role: state.cudRoleReducer,
    }
};
export default connect(mapStateToProps, mapDispatchToProps)(Roles);