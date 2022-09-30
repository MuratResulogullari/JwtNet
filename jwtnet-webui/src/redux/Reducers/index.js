import { combineReducers } from "redux";
import rolesReducer from './roleReducers/rolesReducer';
const rootReducer = combineReducers({
    rolesReducer,
});

export default rootReducer;