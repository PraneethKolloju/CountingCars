import React from 'react'
import Search from './Search'
import Logo from './Logo'
import Login from './Login'
import UserActions from './UserActions'
import { getCurrentUser } from '../actions/authActions'

export default async function Navbar() {
    const user = await getCurrentUser();
    return (
        <header className='sticky top-0 z-50 flex justify-between bg-white shadow-md py-5 px-5 items-center text-gray-800'>
            <Logo />
            <Search />
            {user ? (
                <UserActions user={user} />
            ) : (
                <Login />
            )}

        </header>
    );
}
