import {Grid, Stack, Col, Row, Panel} from "rsuite"
import {CreateLogin} from "../Models/LoginModels/CreateLogin"


const Login = () => {
    return (
            <Grid fluid>
                <Row>
                    <Col xsOffset={11} xs={2} >
            <CreateLogin></CreateLogin>
                        </Col>
                </Row>
            </Grid>
    );
};