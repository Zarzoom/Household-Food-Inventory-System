import {Grid, Stack, Col, Row, Panel} from "rsuite"
import {CreateLogin} from "../Models/LoginModels/CreateLogin"
import {SignIn} from "../Models/LoginModels/SignIn";


const Login = () => {
    return (
            <Grid fluid>
                <Row>
                    <Col xsOffset={11} xs={2} >
            <CreateLogin></CreateLogin><SignIn></SignIn>
                        </Col>
                </Row>
            </Grid>
    );
};

export default Login;