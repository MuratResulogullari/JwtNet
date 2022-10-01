import { combineReducers } from "redux";
import rolesReducer from './roleReducers/rolesReducer';
import cudRoleReducer from './roleReducers/cudRoleReducer';
import usersReducer from './userReducers/usersReducer';
import cudUserReducer from './userReducers/cudUserReducer';
const rootReducer = combineReducers({
    rolesReducer,
    cudRoleReducer,
    usersReducer,
    cudUserReducer
});

export default rootReducer;