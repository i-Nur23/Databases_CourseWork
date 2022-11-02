import React, { Component } from "react";

export class Footer extends Component {

    constructor(props) {
        super(props);
        this.state = {
            year: new Date().getFullYear()
        }
    }

    render() {
        return (
            <footer className="text-center text-lg-start bg-light text-muted p-3 to-bottom">
                <div className="container-fluid container">
                    <div className="d-flex justify-content-between">
                        <p>{this.state.year}</p>
                        <p>Project made by <a href="https://github.com/i-Nur23" style={{ color: "grey" }}>
                            Ainur Iskhakov
                        </a></p>
                    </div>

                </div>
            </footer>
        );
    }
}