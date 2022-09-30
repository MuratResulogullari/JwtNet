import * as actionTypes from '../actions/actionTypes';
import initialState from '../Reducers/initialState';

export default function errorReducer(state = initialState.error, action) {
    switch (action.Type) {
        case actionTypes.GET_ERROR_SUCCESS:
            return action.payload;
        default:
            return state;
    }
}