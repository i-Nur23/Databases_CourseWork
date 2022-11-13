import React, { Component } from 'react';
import Cookies from 'js-cookie';
import { Collapse, Container, Navbar, NavbarBrand, NavItem, NavLink, Nav, Button } from 'reactstrap';
import { Link }  from 'react-router-dom';

function NavMenu(props){

    const logout = async () => {
        await fetch('api/auth', {
            method: 'DELETE',
            headers: {'Content-Type': 'application/json'},
            credentials: 'include',
    })
        props.setToken("");
    };

    let logingForm;
    if (props.token === ''){
        logingForm = (
            <NavLink tag={Link} to="/auth" className='btn btn-info log-form'><i className="bi bi-person-workspace"></i> Войти администратором</NavLink>
        )
    } else {
        logingForm = (
            <NavLink tag={Link} to="/" className='btn btn-danger log-form' onClick={logout}><i className="bi bi-box-arrow-right"></i></NavLink>
        )
    }


    return (
        <header className="sticky-top">
            <Navbar className="navbar-expand-lg navbar-toggleable-sm ng-white border-bottom box-shadow mb-5" style={{ backgroundColor: '#656566' }} light>
                <Container className="container-fluid d-flex justify-content-between">
                    <Nav>
                    <NavbarBrand className="" tag={Link} to="/">
                            <img type = 'image\jpg' src = 'Images\NASCAR_Logo.png' className='img-fluid logo'/>
                    </NavbarBrand>
                    
                    <NavItem>
                        <NavLink tag={Link} to="/participants" className='text-light'>Участники</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} to="/races" className='text-light'>Календарь</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} to="/tracks" className='text-light'>Треки</NavLink>
                    </NavItem>
                    <NavItem>
                        <NavLink tag={Link} to="/results" className='text-light'>Таблица</NavLink>
                    </NavItem>
                    </Nav>
                    <Nav>
                    <NavItem>
                        {logingForm}
                    </NavItem>    
                    </Nav>
                </Container>
            </Navbar>
        </header>
    );
}

export default NavMenu;