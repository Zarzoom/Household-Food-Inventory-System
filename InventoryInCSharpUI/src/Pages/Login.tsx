import {Grid, Stack, Col, Row, Panel} from "rsuite"
import {CreateLogin} from "../Models/LoginModels/CreateLogin"
import {SignIn} from "../Models/LoginModels/SignIn";
import {SignOut} from "../Models/LoginModels/SignOut";


const Login = () => {
    return (
            <Grid fluid>
                <Row>
                    <Col xsOffset={11} xs={2} >
            <CreateLogin></CreateLogin><SignIn></SignIn><SignOut></SignOut>
                        </Col>
                </Row>
            </Grid>
    );
};

export default Login;