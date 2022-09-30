import * as actionTypes from '../../actions/actionTypes';
import initialState from "../initialState";

export default function rolesReducer(state = initialState.roles, action) {
    switch (action.type) {
        case actionTypes.GET_ROLES_SUCCESS:
            return action.payload;
        default:
            return state;
    }
}