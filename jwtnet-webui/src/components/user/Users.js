import React, { Component } from 'react'
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';
import * as userActions from '../../redux/actions/userActions';
import { Link } from 'react-router-dom';
class Users extends Component {
    componentDidMount() {
        this.props.actions.getUsers();
    }
    renderUpdate() {
        return (
            <div>
                {this.props.roles.length > 0 ? this.renderUsers() : this.renderEmpty()}
            </div>
        )
    }
    renderUsers() {
        return (

            <div className='body'>
                <table className='table'>
                    <thead>
                        <tr>
                            <th>#</th>
                            <th>Name</th>
                            <th>Surname</th>
                            <th>Email</th>
                            <th>Process</th>
                        </tr>
                    </thead>
                    <tbody className='table-group-divider'>
                        {this.props.users.map((user) => (
                            <tr key={user.id}>
                                <td>{user.id}</td>
                                <td>{user.name}</td>
                                <td>{user.surname}</td>
                                <td>{user.userName}</td>
                                <td className=' d-flex justify-content-around'>
                                    <button className='btn btn-outline-secondary' onClick={() => this.getUserById(user)}><i className='fa-thin fa-eye'></i></button>
                                    <Link className='btn btn-outline-warning' to={"/saveuser/" + user.id}><i className='fa-thin fa-pen-to-square'></i></Link>
                                    <button className='btn btn-outline-danger' onClick={() => this.deleteUser(user)}><i className='fa-thin fa-xmark'></i></button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        )
    }
    renderEmpty() {
        return (
            <div className='container'>
                <div class="alert alert-warning" role="alert">
                    User is not find
                </div>
            </div>
        )
    }
    render() {
        return (
            <div className='container'>
                <div className='d-flex justify-content-between'>
                    <div><h2>User Managment</h2></div>
                    <div><Link className='btn btn-outline-success' to={"/saveuser"}>New User</Link></div>
                </div>
                {this.props.users.length > 0 ? this.renderUsers() : this.renderEmpty()}
            </div>
        )
    }
}

function mapDispatchToProps(dispatch) {
    return {
        actions: {
            getUsers: bindActionCreators(userActions.getUsers, dispatch),
            getUserById: bindActionCreators(userActions.getUserById, dispatch),
            createUser: bindActionCreators(userActions.createUser, dispatch),
            updateUser: bindActionCreators(userActions.updateUser, dispatch),
            deleteUser: bindActionCreators(userActions.deleteUser, dispatch),

        }
    }
};
function mapStateToProps(state) {

    return {
        users: state.usersReducer,
        user: state.cudUserReducer,
    }
};
export default connect(mapStateToProps, mapDispatchToProps)(Users);