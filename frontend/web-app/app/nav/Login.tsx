'use client'
import { Button } from 'flowbite-react'
import { signIn } from 'next-auth/react'
import React from 'react'

export default function Login() {
    return (
        <div>
            <Button onClick={() => signIn('id-server', {
                redirectTo: '/'
            })}>
                Login
            </Button>
        </div >
    )
}
