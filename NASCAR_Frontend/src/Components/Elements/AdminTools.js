import React from 'react'

function AdminTools(props){
    if (props.token != ''){
        return(
        <div className='float-start rounded-circle'>
            <button
                className='position-fixed btn btn-outline-danger'
                id = "tool-button"
                type='button'
                data-bs-toggle='offcanvas' 
                data-bs-target='#offcanvasExample'
                aria-controls="offcanvasExample"
            >
                <i class="bi bi-tools"></i>
            </button>
            <div
                className='offcanvas offcanvas-start mr-1'
                tabIndex='-1'
                id='offcanvasExample'
                aria-labelledby='offcanvasExampleLabel'
            >
                <div className='offcanvas-header'>
                    <h5 class="offcanvas-title" id="offcanvasExampleLabel">Интсрументы администратора</h5>
                    <button type="button" class="btn-close text-reset" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                </div>
                <div className='offcanvas-body'>
                    <strong>
                    <ul class="list-group list-group-flush">
                    <li class="list-group-item p-3"><a className='black-ref' role='button' href='/addresult'>Добавить результат</a></li>
                    <li class="list-group-item p-3"><a className='black-ref' role='button' href='/addpilot'>Добавить нового пилота</a></li>
                    <li class="list-group-item p-3"><a className='black-ref' role='button' href='/changenum'>Замена машины</a></li>
                    <li class="list-group-item p-3"><a className='black-ref' role='button' href='/changepilotinfo'>Изменить информацию о пилоте</a></li>
                    </ul>
                    </strong>
                </div>
            </div>
        </div>)
    } else {
        return(
            <div className='float-start'></div>
        )
    }
}

export default AdminTools