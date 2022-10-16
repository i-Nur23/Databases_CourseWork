import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';

export class NavMenu extends Component {
    static displayName = NavMenu.name;

    constructor(props) {
        super(props);

        this.toggleNavbar = this.toggleNavbar.bind(this);
        this.spaceBetween = this.spaceBetween.bind(this);
        this.state = {
            collapsed: true
        };
    }

    toggleNavbar() {
        this.setState({
            collapsed: !this.state.collapsed
        });
    }

    spaceBetween() {
        if (!this.state.collapsed) {
            return null;
        } else {
            return (<p>&nbsp;&nbsp;&nbsp;</p>);
        }
    }

    isCenterText() {
        let classText = "text-dark"
        if (this.state.collapsed) {
            return classText;
        } else {
            return classText + " text-center";
        }
    }

    render() {
        return (
            <header className="sticky-top">
                <Navbar className="navbar-expand-lg navbar-toggleable-sm ng-white border-bottom box-shadow mb-5" style={{ backgroundColor: '#b3f2c4' }} light>
                    <Container className="container-fluid d-flex">
                        
                    </Container>
                </Navbar>
            </header>
        );
    }
}