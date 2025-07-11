import React from 'react'
import Search from './Search'
import Logo from './Logo'

export default function NavBar() {
    return (
        <div className="flex justify-between p-3 bg-white sticky top-0 z-50 shadow-md text-gray-800">
            <Logo />
            <Search />
            <div>Login</div>
        </div>
    )
}
