import BaseComponent from 'bootstrap/js/dist/base-component';
import React, { Component } from 'react';
import { UncontrolledCarousel, Carousel, CarouselItem } from 'reactstrap';
//import { Carousel } from 'react-responsive-carousel';
export class HomePage extends Component {
    static displayName = HomePage.name;

    constructor(props) {
        super(props);
        this.WelcomeCarousel = this.WelcomeCarousel.bind(this);
        this.renderHomePage = this.renderHomePage.bind(this);
        this.state = { top5Pilots: [], loading: true };
    }

    componentDidMount() {
        this.populateHome();
    }

    WelcomeCarousel(){
        return(<div>
        <UncontrolledCarousel
                items={[
                {
                    className:'welcome-carousel',
                    altText: 'Slide 1',
                    key: 1,
                    src: '\\Images\\WelcomeCarousel_1.jpg',
                },
                {
                    className:'welcome-carousel',
                    altText: 'Slide 2',
                    key: 2,
                    src: '\\Images\\WelcomeCarousel_2.jpg'
                },
                {
                    className:'welcome-carousel',
                    altText: 'Slide 3',
                    key: 3,
                    src: '\\Images\\WelcomeCarousel_3.jpg'
                },
                {
                    className:'welcome-carousel',
                    altText: 'Slide 4',
                    key: 4,
                    src: '\\Images\\WelcomeCarousel_4.jpg'
                }]} controls={false} indicators={false} autoPlay={true} interval='5000'/></div>);
    }


    renderHomePage(top5Pilots) {
        return (
            <div className='d-flex justify-content-center carDiv'>
                <div className='position-relative'>
                    <this.WelcomeCarousel/>
                </div>
                <div className='position-absolute'>
                    <h1 className='text-c text-dark'>Добро пожаловать в NASCAR</h1>
                </div>
            </div>

            
        )            
    }

    render() {
        let contents = this.state.loading
            ? <div className='d-flex justify-content-center my-auto'>
                <p><em>Loading... please wait.</em></p>
              </div>
            : <this.renderHomePage top5Pilots={this.state.top5Pilots}/>;
        
        return (
            <div className='d-flex justify-content-center'>
                {contents}
            </div>
        );
    }

    async populateHome() {
        const response = await fetch('api');
        console.log(response);
        const data = await response.json();
        this.setState({ top5Pilots: data, loading: false });
    }
}