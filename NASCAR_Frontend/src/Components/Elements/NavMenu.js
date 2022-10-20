import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavItem, NavLink, Nav, Button } from 'reactstrap';
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
                <Navbar className="navbar-expand-lg navbar-toggleable-sm ng-white border-bottom box-shadow mb-5" style={{ backgroundColor: '#656566' }} light>
                    <Container className="container-fluid d-flex">
                        <NavbarBrand className="" tag={Link} to="/">
                                <img type = 'image\jpg' src = 'Images\NASCAR_Logo.png' className='img-fluid logo'/>
                        </NavbarBrand>
                        <Nav>
                        <NavItem>
                            <NavLink tag={Link} to="/" className='text-light'>Пилоты</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} to="/" className='text-light'>Календарь</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} to="/" className='text-light'>Треки</NavLink>
                        </NavItem>
                        <NavItem>
                            <NavLink tag={Link} to="/" className='text-light'>Таблица</NavLink>
                        </NavItem>
                        </Nav>
                        <Nav>
                        <NavItem>
                            <NavLink tag={Link} to="/" className='btn btn-info'><i class="bi bi-person-workspace"></i></NavLink>
                        </NavItem>    
                        </Nav>
                    </Container>
                </Navbar>
            </header>
        );
    }
}