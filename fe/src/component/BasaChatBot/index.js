import React from "react";
import { MDBCard, MDBCardBody, MDBIcon, MDBBtn, MDBTypography, MDBTextArea, MDBCardHeader } from "mdb-react-ui-kit";

const BasaChatBot = () => {
    return (
        <MDBTypography listUnStyled style={{ minHeight: "600px" }}>
            <li className="d-flex justify-content-between mb-4">
                <img
                    src="https://mdbcdn.b-cdn.net/img/Photos/Avatars/avatar-5.webp"
                    alt="avatar"
                    className="rounded-circle d-flex align-self-start me-3 shadow-1-strong"
                    width="60"
                />
                <MDBCard>
                    <MDBCardHeader className="d-flex justify-content-between p-3">
                        <p className="fw-bold mb-0">Si nyAI</p>
                        <p className="text-muted small mb-0">
                            <MDBIcon far icon="clock" /> Just Now
                        </p>
                    </MDBCardHeader>
                    <MDBCardBody>
                        <p className="mb-0">
                            Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque
                            laudantium.
                        </p>
                    </MDBCardBody>
                </MDBCard>
            </li>
            <li className="bg-white mb-3">
                <MDBTextArea label="Message" id="textAreaExample" rows={4} />
            </li>
            <MDBBtn color="info" rounded className="float-end">
                Send
            </MDBBtn>
        </MDBTypography>
    );
};

export default BasaChatBot;
