import * as actionTypes from '../../actions/actionTypes';
import initialState from "../initialState";

export default function cudRoleReducer(state = initialState.role, action) {
    switch (action.Type) {
        case actionTypes.CREATE_ROLE_SUCCESS:
            return action.payload;
        case actionTypes.UPDATE_ROLE_SUCCESS:
            return action.payload;
        case actionTypes.DELETE_ROLE_SUCCESS:
            return action.payload;
        case actionTypes.GET_ROLE_BY_ID_SUCCESS:
            return action.payload;
        default:
            return state;
    }

}
