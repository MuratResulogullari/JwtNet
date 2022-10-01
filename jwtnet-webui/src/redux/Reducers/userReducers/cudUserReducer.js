import * as actionTypes from '../../actions/actionTypes';
import initialState from "../initialState";

export default function cudUserReducer(state = initialState.user, action) {
    switch (action.Type) {
        case actionTypes.CREATE_USER_SUCCESS:
            return action.payload;
        case actionTypes.UPDATE_USER_SUCCESS:
            return action.payload;
        case actionTypes.DELETE_USER_SUCCESS:
            return action.payload;
        case actionTypes.GET_USER_BY_ID_SUCCESS:
            return action.payload;
        default:
            return state;
    }

}
