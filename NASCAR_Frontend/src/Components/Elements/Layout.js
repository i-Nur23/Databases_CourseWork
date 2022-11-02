import React, { Component } from 'react';
import { Container } from 'reactstrap';
import NavMenu from './NavMenu';
import { Footer } from './Footer';
import AdminTools from './AdminTools';

export class Layout extends Component {
    static displayName = Layout.name;

    render() {
        return (
            <div id="page-container">
                <NavMenu token={this.props.token} setToken={this.props.setToken}/>
                <AdminTools token={this.props.token}/>
                <Container id = "content-container">
                    {this.props.children}
                </Container>
                <Footer />
            </div>
        );
    }
}