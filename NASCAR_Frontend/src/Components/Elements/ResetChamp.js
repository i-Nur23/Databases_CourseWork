import React, {useState, useEffect } from "react";
import {Modal, ModalHeader, ModalBody, ModalFooter} from 'reactstrap';

const ResetChamp = () => {
    const [pilots, setPilots] = useState([]);
    const [checkbuttons, setCheckbuttons] = useState([]);
    const [show, setShow] = useState(false);

    useEffect(() => {
        (
            async () => {

                const response = await fetch('api/reset', {
                    headers: {'Content-Type': 'application/json'},
                    credentials: 'include',
                });

                const content = await response.json();

                setPilots(content.pilots);
                setCheckbuttons(new Array(content.pilots.length).fill(false));
            }
        )();
    }, []);
    
    const toggle = () => setShow(!show);
    
    const changeCheckedNumbers = position => {
        const updatedCheckedState = checkbuttons.map((item, index) =>
            index === position ? !item : item
        );

        setCheckbuttons(updatedCheckedState);
    }
    
    const reset = async () => {
        var arrOfIds = [];
        checkbuttons.map( (checked, index) => {
            if (checked)
            {
                arrOfIds.push(pilots[index].id);
            }
        })

        await fetch('api/reset', {
            method: 'DELETE', headers: {
                'Content-Type': 'application/json',
                'Accept': 'application/json',
            },
            
            body: JSON.stringify(arrOfIds),
        });
        
    }

    return(
        <div>
            <div className='d-flex justify-content-center' style={{marginTop:'200px'}}>
                <div>
                    <center><h3>Чемпионат завершен</h3></center>
                    <center><button type="button" className="btn btn-danger" data-toggle="modal"
                                    data-target="#deletePilotsModal" onClick={toggle}>
                        Начать заново
                    </button></center>
                </div>
            </div>

            <Modal isOpen={show} toggle={toggle}>
                <ModalHeader>
                    Выберите пилотов, которых вы хотите удалить перед новым сезоном
                </ModalHeader>
                <ModalBody>
                    <div className="btn-group-vertical w-100" role="group" aria-label="Basic checkbox toggle button group">
                        {pilots.map((pilot, index) => (
                            <div className='d-flex justify-content-center w-100'>
                                    <input type="checkbox" className="btn-check" id={`btncheck${index}`} 
                                           checked={checkbuttons[index]}
                                           onChange={() => changeCheckedNumbers(index)}
                                    />
                                    <label className="btn btn-outline-danger border border-start-0 border-end-0 
                                    border-top-0 rounded-0 w-100" htmlFor={`btncheck${index}`}>{pilot.name} {pilot.surName}</label>
                            </div>
                        ))} 
                    </div>
                </ModalBody>
                <ModalFooter>
                    <button className='btn btn-light' onClick={toggle}>
                        Отмена
                    </button>
                    <button className='btn btn-dark' onClick={reset}>
                        Начать заново
                    </button>
                </ModalFooter>
            </Modal>
        </div>
    )
}

export default ResetChamp