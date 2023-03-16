import React, { useState } from "react";
import { MDBRow, MDBCol, MDBCard, MDBCardBody, MDBTypography } from "mdb-react-ui-kit";
import BasaTranslator from "component/BasaTranslator";
import BasaChatBot from "component/BasaChatBot";

function Fitur() {
    const [isBot, setIsBot] = useState(false);
    const [active, setActive] = useState(1);

    const clickBot = (changeToBot) => {
        setIsBot(changeToBot);
        const isActive = changeToBot ? 2 : 1;
        setActive(isActive);
    };

    return (
        <MDBCard>
            <MDBCardBody>
                <MDBRow>
                    <MDBCol md="6" lg="5" xl="4" className="mb-4 mb-md-0">
                        <h5 className="font-weight-bold mb-3 text-center text-lg-start">Fitur Chat</h5>
                        <MDBCard>
                            <MDBCardBody>
                                <MDBTypography listUnStyled className="mb-0">
                                    <li
                                        className="p-2 border-bottom"
                                        style={{ backgroundColor: active === 1 ? "#eee" : "" }}
                                    >
                                        <a
                                            href="#!"
                                            className="d-flex justify-content-between"
                                            onClick={() => clickBot(false)}
                                        >
                                            <div className="d-flex flex-row">
                                                <img
                                                    src="https://mdbcdn.b-cdn.net/img/Photos/Avatars/avatar-8.webp"
                                                    alt="avatar"
                                                    className="rounded-circle d-flex align-self-center me-3 shadow-1-strong"
                                                    width="60"
                                                />
                                                <div className="pt-1">
                                                    <p className="fw-bold mb-0">Translator</p>
                                                    <p className="small text-muted">Dupi tiasa abdi bantos?</p>
                                                </div>
                                            </div>
                                            <div className="pt-1">
                                                <p className="small text-muted mb-1">Just now</p>
                                                <span className="badge bg-danger float-end">1</span>
                                            </div>
                                        </a>
                                    </li>
                                    <li
                                        className="p-2 border-bottom"
                                        style={{ backgroundColor: active === 2 ? "#eee" : "" }}
                                    >
                                        <a
                                            href="#!"
                                            className="d-flex justify-content-between"
                                            onClick={() => clickBot(true)}
                                        >
                                            <div className="d-flex flex-row">
                                                <img
                                                    src="https://mdbcdn.b-cdn.net/img/Photos/Avatars/avatar-5.webp"
                                                    alt="avatar"
                                                    className="rounded-circle d-flex align-self-center me-3 shadow-1-strong"
                                                    width="60"
                                                />
                                                <div className="pt-1">
                                                    <p className="fw-bold mb-0">Si nyAI</p>
                                                    <p className="small text-muted">hayu diajar nyunda.</p>
                                                </div>
                                            </div>
                                            <div className="pt-1">
                                                <p className="small text-muted mb-1">Yesterday</p>
                                            </div>
                                        </a>
                                    </li>
                                </MDBTypography>
                            </MDBCardBody>
                        </MDBCard>
                    </MDBCol>

                    <MDBCol md="6" lg="7" xl="8">
                        {isBot ? <BasaChatBot /> : <BasaTranslator />}
                    </MDBCol>
                </MDBRow>
            </MDBCardBody>
        </MDBCard>
    );
}

export default Fitur;
