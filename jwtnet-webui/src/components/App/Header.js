import React, { Component } from 'react'
import './Header.css'

export default class Header extends Component {
    render() {
        return (
            <div className='header'>
                <nav class=" navbar navbar-expand-lg ">
                    <div class="container-fluid">
                        <a class="navbar-brand" href="#"><h2>brand</h2></a>
                        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                            <span class="navbar-toggler-icon"></span>
                        </button>
                        <div class="collapse navbar-collapse d-flex justify-content-end" id="navbarSupportedContent">
                            <ul class="navbar-nav me-auto mb-2 mb-lg-0 d-flex">
                                <li class="nav-item">
                                    <a class="nav-link active" aria-current="page" href="#">Home</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="/roles">Roles</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="/users">Users</a>
                                </li>
                                <li class="nav-item dropdown d-flex justify-content-between">
                                    <a class="nav-link dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                        Account
                                    </a>
                                    <ul class="dropdown-menu">
                                        <li><a class="dropdown-item" href="#">Action</a></li>
                                        <li><a class="dropdown-item" href="#">Another action</a></li>
                                        <li><hr class="dropdown-divider" /></li>
                                        <li><a class="dropdown-item" href="#">logout</a></li>
                                    </ul>
                                </li>

                            </ul>

                        </div>
                    </div>
                </nav>
            </div>
        )
    }
}
