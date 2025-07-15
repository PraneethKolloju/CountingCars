'use client'
import { Button, Dropdown, DropdownHeader, DropdownItem } from 'flowbite-react'
import { User } from 'next-auth'
import { signOut } from 'next-auth/react'
import Link from 'next/link'
import React from 'react'
import { AiFillCar, AiFillTrophy, AiOutlineLogout } from 'react-icons/ai'
import { HiCog, HiUser } from 'react-icons/hi'

type Props =
    {
        user: User
    }

export default function UserActions({ user }: Props) {
    return (
        <div>
            <Dropdown inline label={`Welcome ${user.name}`} className='cursor-pointer'>
                <DropdownItem icon={HiUser}>
                    My Auctions
                </DropdownItem>
                <DropdownItem icon={AiFillTrophy}>
                    Auctions Won
                </DropdownItem>
                <DropdownItem icon={AiFillCar}>
                    Sell Car
                </DropdownItem>
                <DropdownItem icon={HiCog}>
                    <Link href='/session'>
                        Session(for dev!)
                    </Link>
                </DropdownItem>
                <DropdownHeader />
                <DropdownItem icon={AiOutlineLogout} onClick={() => signOut({ redirectTo: '/' })}>
                    Logout
                </DropdownItem>

            </Dropdown>
        </div>
    )
}
