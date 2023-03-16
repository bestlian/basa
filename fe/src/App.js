import React from "react";
import { MDBContainer, MDBRow, MDBCol } from "mdb-react-ui-kit";
import Fitur from "./view/Fitur";

function App() {
    return (
        <MDBContainer fluid className="py-5" style={{ backgroundColor: "#eee" }}>
            <MDBRow end>
                <MDBCol md="8">
                    <Fitur />
                </MDBCol>
            </MDBRow>
        </MDBContainer>
    );
}

export default App;
